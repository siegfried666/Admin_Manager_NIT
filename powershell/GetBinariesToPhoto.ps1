Get-ADUser user -Properties thumbnailPhoto | Select-Object -ExpandProperty thumbnailphoto | Set-Content -Path C:\path.jpg -Encoding Byte