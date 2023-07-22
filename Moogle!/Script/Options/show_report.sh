show_report() {
    ans=($1)
    cd ..
    cd ..
    cd 'Informe del Proyecto'

    if [ ! -f 'Informe del proyecto'.pdf ]
    then
    pdflatex 'Informe del proyecto'.tex
    fi

    if [ $# -gt 0 ] 
    then
    $ans 'Informe del proyecto'.pdf
    else
    xdg-open 'Informe del proyecto'.pdf
    fi
}

while true 
do
echo "Do you want to use the default viewer? [y/n -> (yes/no)]"
read a
if [[ $a = "" ]]
then
echo -e "Try again\n"
elif [ $a = "y" ] 
then 
show_report
break
elif [ $a = "n" ]
then
while true
do
echo -e "Type the command to open the file wiewer that you want (without the filename, \nbecause it is automatically added at the end of the command)"
read b
if [[ $b = "" ]]
then
echo -e "Try again \n"
else
show_report $b
break
fi
done
break
else 
echo -e "You did not select a valid token \n"
fi
done
