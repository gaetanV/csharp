ROOT="$PWD/"
C="$PWD/C/"
NAME="imagemagic"

function dll {
    cd $C
    gcc -c $NAME.c 
    gcc -shared -o "$NAME.dll" "$NAME.o"
    cd $ROOT;
}

function serve {
    dotnet run
}

case "${1}" in
    dll)
        dll
        ;;
    compile) 
        dll
        dotnet restore
        serve
        ;;
    serve) 
        serve
        ;;   
    *)
        echo $"compile | serve | dll "
        exit 1
esac

