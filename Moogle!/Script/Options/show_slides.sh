show_slides() {
    ans=($1)
    cd ..
    cd ..
    cd 'Presentación del proyecto'

    if [ ! -f 'Moogle'.pdf ]
    then
    pdflatex 'Moogle'.tex
    fi
    
    if [ $# -gt 0 ] 
    then
    $ans Moogle.pdf
    else
    xdg-open Moogle.pdf
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
show_slides
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
show_slides $b
break
fi
done
break
else 
echo -e "You did not select a valid token \n"
fi
done
