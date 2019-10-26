# gui-patterns
Project which tries to automate some often-used parts of a GUI.

## Features

### None
This project is in its infancy.

## In development

### Input validation and parsing patterns
Pattern in which a textbox is used as an input for some other type `T` (for example a number). While editing, the textbox should show whether the input currently can be parsed. Through code, it should be possible to get a `T option` which contains the parsed value if parsing succeeded.