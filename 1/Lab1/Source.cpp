#include <iostream>
#include <cstring>
#include <vector>
#include <fstream>
using namespace std;

#define INF 9999999


int main()
{
    ifstream myfile("matrix.txt");
    int size;
    myfile >> size;

    if (myfile.is_open())
    {
        vector<vector<int>> matrix(size, vector<int>(size));

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                myfile >> matrix[i][j];
            }
        }

        int no_edge;
        vector<int>selected(size);

        no_edge = 0;

        selected[0] = true;

        int x;
        int y;

        cout << "Edge" << " : " << "Weight";
        cout << endl;
        int result_weight = 0;

        while (no_edge < size - 1) {

            int min = INF;
            x = 0;
            y = 0;

            for (int i = 0; i < size; i++) {
                if (selected[i]) {
                    for (int j = 0; j < size; j++) {
                        if (!selected[j] && matrix[i][j]) {
                            if (min > matrix[i][j]) {
                                min = matrix[i][j];
                                x = i;
                                y = j;
                            }

                        }
                    }
                }
            }
            cout << x << " - " << y << " :  " << matrix[x][y];
            result_weight += matrix[x][y];
            cout << endl;
            selected[y] = true;
            no_edge++;
        }
        cout << endl;
        cout << "Result weight: " << result_weight << endl;
    }
    else
    {
        cout << "File couldn't be open";
    }
    return 0;
}