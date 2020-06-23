module Feliz.MediaQuery

open System
open Browser
open Browser.Types
open Feliz
   
type [<AllowNullLiteral>] MediaQueryListEvent =    
    inherit Event
    abstract matches: bool with get, set
    abstract media: string with get, set

[<AutoOpen>]
module UseMediaQueryExtension =
     type React with
        static member useMediaQuery(query) =
            let mediaMatch = window.matchMedia(query)
            let (matches, setMatches) = React.useState(mediaMatch.matches)
            
            React.useEffect(fun () ->
                let handler = fun (e: MediaQueryListEvent) -> setMatches(e.matches)
                mediaMatch.addEventListener("change", fun e ->
                    let x = unbox<MediaQueryListEvent>(e)
                    handler x)
                { new IDisposable with member this.Dispose() =
                                        mediaMatch.removeEventListener("change", fun e -> handler (unbox<MediaQueryListEvent>(e))) }
                )
            
            matches
