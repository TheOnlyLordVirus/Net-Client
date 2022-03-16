#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "HTTPRequest.hpp"
#define POST_VALUES "host=%s&user=%s&pass=%s&name=%s&username=%s&password=%s&cheese=%s&parms=%s"
#define DB_HOST "167.99.37.167"
#define DB_USER "root"
#define DB_PASS ""
#define DB_NAME "user_info_db"
#define USERNAME "pastafarian"
#define PASSWORD "cheesetoast"
#define CHEESE "add_user"
#define PARM "{\"email\": \"test123@mail.com\", \"username\": \"New Test\", \"password\": \"pass123\", \"ip_addy\": \"0.0.0.0\", \"admin\": \"true\", \"time_value\": \"7\"}"

#pragma warning(disable:4996)

using namespace std;

void apiAssistant()
{

}

// Entry point.
int main(int argc, char* args[])
{

	// call socket listenr, when input buffer is returned from packet data, we then will send the buffer params to apiAssistant 

    apiAssistant();

	std::string inputbuffer = "";

	std::cin >> inputbuffer;

	return 0;
}