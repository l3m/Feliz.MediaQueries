module Feliz.MediaQuery

open System
open Browser
open Browser.Types
open Feliz
open Fable.Core.JsInterop
(*import {useEffect, useState} from 'react';

export const useMediaQuery = (query) => {
  const mediaMatch = window.matchMedia(query);
  const [matches, setMatches] = useState(mediaMatch.matches);

  useEffect(() => {
    const handler = e => setMatches(e.matches);
    mediaMatch.addListener(handler);
    return () => mediaMatch.removeListener(handler);
  });
  return matches;
};*)
open System.Runtime.CompilerServices

   
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
