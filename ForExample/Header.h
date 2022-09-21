#ifndef FOREXAMPLE_HEADER_H
#define FOREXAMPLE_HEADER_H

#include <iostream>
#include <cmath>
#include <vector>


class Digit{
private:
    std::vector<std::pair<int, int>> dels;
    int index = -1;
    int result = 1;
    int A;
    void multipliers(int A); //Разложение на множители
public:
    Digit() = default;
    Digit(const Digit &elem) = default;
    Digit(Digit &&elem) = default;
    ~Digit() = default;

    void find_result(int A); //Подсчет числа
    void show_result();
};

#endif