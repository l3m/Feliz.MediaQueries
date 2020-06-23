module App

open Feliz
open Elmish

type State = { Count: int }

type Msg =
    | Increment
    | Decrement

let init() = { Count = 0 }, Cmd.none

let update (msg: Msg) (state: State) =
    match msg with
    | Increment -> { state with Count = state.Count + 1 }, Cmd.none
    | Decrement -> { state with Count = state.Count - 1 }, Cmd.none

open Feliz.MediaQuery
let mqTest = React.functionComponent(fun () ->
    let (count, setCount) = React.useState(0)
    let matches500Plus = React.useMediaQuery("(min-width: 500px)")

    // execute this effect on every render cycle
    React.useEffect(fun () -> Browser.Dom.document.title <- sprintf "Count = %d" count)

    Html.div [
        Html.h1 count
        Html.button [
            if matches500Plus then 
                prop.text "Large"
            else
                prop.text "Small"
            prop.onClick (fun _ -> setCount(count + 1))
        ]
    ])
let render (state: State) (dispatch: Msg -> unit) =
    Html.div [
        Html.button [
            prop.onClick (fun _ -> dispatch Increment)
            prop.text "Increment"
        ]

        Html.button [
            prop.onClick (fun _ -> dispatch Decrement)
            prop.text "Decrement"            
        ]
        
        Html.div [
            prop.children [
                mqTest ()
            ]
        ]

        Html.h1 state.Count
        
    ]
