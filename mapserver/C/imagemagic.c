#include <stdio.h>
#include <string.h>

int image(char *buffer){
    strncpy(buffer, "hello hello hello hello hello hello hello hello hello hello", 256);
    return 1;
}

int unsafeimage(char *buffer){
    strncpy(buffer, "hello hello hello hello hello hello hello hello hello hello", 256);
    return 1;
}