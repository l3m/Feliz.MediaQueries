module Feliz.MediaQuery

open System
open Browser
open Browser.Types
open Feliz

type ScreenSize =
    | Mobile
    | MobileLandscape
    | Tablet
    | Desktop
    | Widescreen

type Breakpoints = {
    MobileLandscape: int
    Tablet: int
    Desktop: int
    Widescreen: int
}

let defaultBreakpoints = {
    MobileLandscape = 576
    Tablet = 768
    Desktop = 1024
    Widescreen = 1216
}

let makeQueries breakpoints =
    let mobileQuery = sprintf "(max-width: %ipx)" breakpoints.MobileLandscape
    let mobileLandscapeQuery = sprintf "(max-width: %ipx)" breakpoints.Tablet
    let tabletQuery = sprintf "(max-width: %ipx)" breakpoints.Desktop
    let desktopQuery = sprintf "(max-width: %ipx)" breakpoints.Widescreen
    let widescreenQuery = sprintf "(min-width: %ipx)" <| breakpoints.Widescreen + 1
    mobileQuery, mobileLandscapeQuery, tabletQuery, desktopQuery, widescreenQuery

[<AutoOpen>]
module UseMediaQueryExtension =
     type React with
        static member useMediaQuery (query: string) =
            let (mq, setMq) = React.useState(fun () -> window.matchMedia(query))

            React.useEffect(fun () ->
                mq.addEventListener("change", fun e ->
                    let mqEvent = unbox<MediaQueryList>(e)
                    setMq mqEvent)
                {new IDisposable with
                     member this.Dispose() =
                        mq.removeEventListener("change", fun _ -> ())}

            , [| query :> obj |])

            mq.matches

        static member useResponsive (breakpoints: Breakpoints) =
            let m, l, t, d, w = makeQueries breakpoints
            let mx = React.useMediaQuery m
            let lx = React.useMediaQuery l
            let tx = React.useMediaQuery t
            let dx = React.useMediaQuery d
            let wx = React.useMediaQuery w
            match mx, lx, tx, dx, wx with
            | true, _, _, _, _ -> ScreenSize.Mobile
            | _, true, _, _, _ -> ScreenSize.MobileLandscape
            | _, _, true, _, _ -> ScreenSize.Tablet
            | _, _, _, true, _ -> ScreenSize.Desktop
            | _ -> ScreenSize.Widescreen
