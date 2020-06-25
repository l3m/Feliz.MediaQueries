module Feliz.MediaQuery

open System
open Browser
open Browser.Types
open Feliz

type ScreenWidth =
    | Mobile
    | Tablet
    | Desktop
    | Widescreen

type Breakpoints = {
    Tablet: string
    Desktop: string
    Widescreen: string
}

let defaultBreakpoints = {
    Tablet = "768px"
    Desktop = "1024px"
    Widescreen = "1216px"
}

[<AutoOpen>]
module UseMediaQueryExtension =
     type React with
        static member useMediaQuery(query: string) =
            let (mqList, setMqList) = React.useState(fun () -> window.matchMedia(query))

            React.useEffect(fun () ->
                mqList.addEventListener("change", fun e ->
                    let mqE = unbox<MediaQueryList>(e)
                    setMqList mqE)
                {new IDisposable with
                     member this.Dispose() =
                        mqList.removeEventListener("change", fun _ -> ())}

            , [| query :> obj |])

            mqList.matches
