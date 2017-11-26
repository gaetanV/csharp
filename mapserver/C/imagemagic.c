#include <stdio.h>
#include <string.h>

int main(int argc, char *argv[]) {
    printf("C");
    printf(" is magic");
    return 1;
}

int image(char *buffer){
    strncpy(buffer, "hello", sizeof(buffer));
    return 1;
}