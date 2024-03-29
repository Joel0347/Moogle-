clean() {
    cd ..
    cd ..

    # eliminamos los archivos auxiliares del informe.
    cd 'Informe del Proyecto'
    rm -f *.aux *.lot *.lof *.log *.toc *.dvi *.ps *.bbl *.out *.synctex.gz *.fls *.fdb_latexmk

    cd ..

    # eliminamos carpetas auxiliares de MoogleServer y MoogleEngine.
    cd MoogleServer
    rm -r bin obj
    cd ..
    cd MoogleEngine
    rm -r bin obj 
    cd ..
    # eliminamos los archivos auxiliares de la presentacion
    cd 'Presentación del proyecto'
    rm -f *.aux *.lot *.lof *.log *.toc *.dvi *.ps *.bbl *.out *.synctex.gz *.fls *.fdb_latexmk *.nav *.snm *.vrb
    cd sections
    rm -f *.aux
}
clean