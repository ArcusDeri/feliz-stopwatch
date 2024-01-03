open Browser.Dom
open Feliz

open FelizStopwatch.Client

[<EntryPoint>]
let main _ =
    let root = ReactDOM.createRoot(document.getElementById "feliz-app")
    root.render(Components.Counter())
    0