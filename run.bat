for /f "delims=" %%A in ('cd') do (
     set foldername=%%~nxA
    )
copy bin\%foldername%.dll F:\Steam\steamapps\common\TheLongDark\mods\%foldername%.dll
cd F:\Steam\steamapps\common\TheLongDark\
F:
tld.exe -popupwindow