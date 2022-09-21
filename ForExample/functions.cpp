#include "Header.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <iostream>

void Digit::multipliers(int a){
    int i = 2;
    while (i * i <= a){
        if(a % i == 0){
            dels.push_back({i, 0});
            index++;
        }
        while(a % i == 0){
            a /= i;
            dels[index].second++;
        }
        i++;
    }
    if(a != 1){
        dels.push_back({a, 1});
    }
}

void Digit::find_result(int A){
    if(A >= 1){
        this->A = A;
        multipliers(A);
        for(int i = 0; i < size(dels); i++){
            if(dels[i].second > 0){
                result *= (int)pow(dels[i].first, dels[i].second % 2 == 0? dels[i].second / 2: (dels[i].second + 1) / 2);
            }
        }
    }
}

void Digit::show_result(){
    if(result != 1 || A == 1){
        std::cout << result;
    }
    else{
        std::cout << "Такого числа не существует";
    }
    std::cout << std::endl;
}