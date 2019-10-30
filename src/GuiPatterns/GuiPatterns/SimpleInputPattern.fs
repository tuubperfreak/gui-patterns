// MIT License
// Copyright (c) tuubperfreak
// https://github.com/tuubperfreak/gui-patterns

module GuiPatterns.SimpleInputPattern

open System

type EventDelegate = delegate of obj * EventArgs -> unit

type IControl =
    abstract member Control: obj with get

type ValueControlState =
| Valid
| Invalid of Reason: string

type ILabeledValueControl<'TOutput> =
    inherit IControl
    abstract member Value: 'TOutput with get, set
    abstract member LabelText: string with get, set
    abstract member State: ValueControlState with get, set
    [<CLIEvent>]
    abstract member ValueChanged: IEvent<EventDelegate, EventArgs>

type ICheckBox =
    inherit ILabeledValueControl<bool>

type ITextBox =
    inherit ILabeledValueControl<string>

type IChoiceBox = // todo: this type should restrict Value to one of the Choices
    inherit ILabeledValueControl<string>
    abstract member Choices: List<string> with get, set

type IButton =
    inherit IControl
    abstract member ButtonText: string with get, set
    [<CLIEvent>]
    abstract member Click: IEvent<EventDelegate, EventArgs>

type ILabeledValueControlFactory =
    abstract member CreateCheckBox: unit -> ICheckBox
    abstract member CreateTextBox: unit -> ITextBox
    abstract member CreateChoiceBox: unit -> IChoiceBox

type ParsingControl<'TOutput> (textBox: ITextBox, tryParse: string -> Result<'TOutput, string>) =

    do
        let handler _ _ =
            let result = tryParse textBox.Value
            match result with
            | Ok _ -> textBox.State <- Valid
            | Error reason -> textBox.State <- Invalid reason
        textBox.ValueChanged.AddHandler(EventDelegate(handler))

    member this.RawText with get () = textBox.Value

    member this.Result
        with get () =
            let result = tryParse textBox.Value
            match result with
            | Error reason -> textBox.State <- Invalid reason
            | Ok _ -> textBox.State <- Valid
            result

    interface IControl with
        member this.Control with get () = textBox.Control

type IInputControl =
    inherit IControl
    abstract member Value: Result<obj, string> with get

let createInputControlForType (t: Type) =
    match t with
    | _ when t = typeof<int> -> () // todo: implement for most primitive types.
    | _ -> ()
