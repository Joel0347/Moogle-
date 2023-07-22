while  true 
do
    echo -e "Write down the number of the option you want: \n1-> run \n2-> report \n3-> slides \n4-> show_report \n5-> show_slides \n6-> clean \n7-> exit" 
    read op

    if [[ ! $op =~ ^[0-9]+$ ]]
    then
    echo -e "\n"
    elif [ $op -eq 1 ] 
    then 
    cd Options; bash run.sh; cd ..

    elif [ $op -eq 2 ]
    then
    cd Options; bash report.sh; cd ..

    elif [ $op -eq 3 ]
    then
    cd Options; bash slides.sh; cd ..

    elif [ $op -eq 4 ]
    then
    cd Options; bash show_report.sh; cd ..

    elif [ $op -eq 5 ]
    then
    cd Options; bash show_slides.sh; cd ..

    elif [ $op -eq 6 ]
    then
    echo -e "You have cleaned the unnecesary files \n"
    cd Options; bash clean.sh; cd ..

    elif [ $op -eq 7 ]
    then
    exit

    else
    echo -e "You did not select any of the given numbers \n"
    fi
done


    
