module App

open Feliz
open Feliz.MediaQuery

let mqTest = React.functionComponent(fun () ->
    let (count, setCount) = React.useState(0)
    let width = React.useResponsive(defaultBreakpoints)

    Html.div [
        Html.div [
            prop.text (width.ToString())
            prop.onClick (fun _ -> setCount(count + 1))
        ]
    ])

open Browser.Dom
ReactDOM.render(mqTest, document.getElementById "feliz-app")
