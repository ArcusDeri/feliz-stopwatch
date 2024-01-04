open Browser.Dom
open Feliz

open FelizStopwatch.Client.Stopwatch

[<EntryPoint>]
let main _ =
    let root = ReactDOM.createRoot (document.getElementById "feliz-stopwatch-app")
    root.render (Stopwatch())
    0
