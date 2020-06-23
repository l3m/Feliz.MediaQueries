module Main

open Fable.Core.JsInterop

importAll "../styles/main.scss"
open Feliz


open Browser.Dom
ReactDOM.render(App.mqTest, document.getElementById "feliz-app")
