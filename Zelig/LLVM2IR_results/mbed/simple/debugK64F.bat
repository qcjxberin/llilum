@echo off

"%dp~0%debug.bat" K64F\mbed_simple.elf %* -ex "target remote :3333" -ex "monitor halt reset" -ex "ni"

exit /b
