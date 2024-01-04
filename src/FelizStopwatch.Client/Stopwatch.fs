module FelizStopwatch.Client.Stopwatch

open System

open Browser.Dom
open Elmish
open Feliz
open Feliz.UseElmish

type Msg =
    | Start
    | Reset
    | Stop

type Status =
    | Zero
    | Running
    | Stopped

type State =
    { currentDate: DateTime option
      status: Status }

let init () =
    { currentDate = None; status = Zero }, Cmd.none

let update msg (state: State) =
    match msg with
    | Start ->
        match state.status with
        | Zero ->
            { state with
                currentDate = Some DateTime.Now
                status = Running },
            Cmd.none
        | Running -> state, Cmd.none
        | Stopped -> { state with status = Running }, Cmd.none
    | Reset ->
        match state.status with
        | Zero -> state, Cmd.none
        | Running
        | Stopped ->
            { state with
                currentDate = None
                status = Zero },
            Cmd.none
    | Stop ->
        match state.status with
        | Zero -> state, Cmd.none
        | Running -> { state with status = Stopped }, Cmd.none
        | Stopped -> state, Cmd.none


[<ReactComponent>]
let Stopwatch () =
    let elapsedPlaceholder = "00:00:000"
    let state, dispatch = React.useElmish (init, update, [||])
    let elapsed, setElapsed = React.useState elapsedPlaceholder

    React.useEffect (
        (fun () ->
            let id =
                window.setInterval (
                    (fun () ->
                        if state.status = Running then
                            match state.currentDate with
                            | Some d ->
                                let diff = DateTime.Now - d
                                setElapsed (sprintf "%02i:%02i:%03i" diff.Minutes diff.Seconds diff.Milliseconds)
                            | _ -> failwith "shouldn't happen"),
                    10
                )

            { new IDisposable with
                member _.Dispose() = window.clearInterval id }),
        [| box state.status |]
    )

    Html.div
        [ Html.h1 elapsed
          Html.button [ prop.onClick (fun _ -> dispatch Start); prop.text "Start" ]
          Html.button [ prop.onClick (fun _ -> dispatch Stop); prop.text "Stop" ]
          Html.button
              [ prop.onClick (fun _ ->
                    // avoid text flicker to last value before "Reset" was clicked when starting new stopwatch after Reset
                    setElapsed elapsedPlaceholder
                    dispatch Reset)
                prop.text "Reset" ] ]
