```
Emulate keyboard strokes through Interception driver.

Usage:
    typewriter            Show help message and activate echo mode
    typewriter <strokes>  Run strokes with AHK syntax

For details about the AHK syntax, see:
    https://www.autohotkey.com/docs/commands/Send.htm

Unsupported strokes:
    {Browser_Back} and similar multimedia keys
    {Click}, {LButton} and other mouse strokes
    {Blind}, {Raw}, {Text}
    {ASC nnnnn}, {U+nnnn}, {vkXX}, {vkXXscYYY}

Special strokes:
    {s<n>}   sleep for n ms
    {i<n>}   set the interval between two strokes to n ms
             default interval is 10 ms
    {sc<x>}  send key with custom scancode

Note:
    A connected keyboard is required.
    The program assumes a US English input method.


Examples:

    typewriter ""^+{Esc}""
        Send Ctrl-Shift+Esc to open Task Manager
        Double quotes are used to escape ^ character

    typewriter #rcontrol{Enter}
        Open control panel through Win-R

    typewriter {s1000}{i0}{Up 10}
        Sleep 1 s then type Up arrow 10 times with no delay

    typewriter +{sc86}
        Type the extra \ key on ISO keyboard while holding Shift

    typewriter {Space down}{s500}{Space up}
        Hold Space bar for 500 ms
```