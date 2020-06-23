module App

open Feliz
open Feliz.MediaQuery

let mqTest = React.functionComponent(fun () ->
    let (count, setCount) = React.useState(0)
    let matchesQuery = React.useMediaQuery("(min-width: 500px)")

    // execute this effect on every render cycle
    React.useEffect(fun () -> Browser.Dom.document.title <- sprintf "Count = %d" count)

    Html.div [
        Html.div [
            if matchesQuery then 
                prop.text "Large"
            else
                prop.text "Small"
            prop.onClick (fun _ -> setCount(count + 1))
        ]
    ])

open Browser.Dom
ReactDOM.render(mqTest, document.getElementById "feliz-app")
