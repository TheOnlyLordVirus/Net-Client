#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <iostream>
#include <cstring>
#include <cstdio>
#include <cstdlib>

#include "HTTPRequest.hpp"

#define POST_VALUES "host=%s&user=%s&pass=%s&name=%s&username=%s&password=%s&cheese=%s&parms=%s"

#define DB_HOST "localhost"
#define DB_USER "admin"
#define DB_PASS "Kush986753421"
#define DB_NAME "USER_INFO_DB"

#define USERNAME "pastafarian"
#define PASSWORD "cheesetoast"

#define CHEESE "add_user"
#define PARM "{\"email\": \"test123@mail.com\", \"username\": \"New Test\", \"password\": \"pass123\", \"ip_addy\": \"0.0.0.0\", \"admin\": \"true\", \"time_value\": \"7\"}"

#pragma warning(disable:4996)

using namespace std;

void apiAssistant()
{
	std::cout << "socketSender()" << std::endl;

	try
	{
		std::cout << "try post" << std::endl;

		char connection_buffer[0xFFFF];
		memset(connection_buffer, 0, 0xFFFF);
		sprintf(connection_buffer, POST_VALUES, DB_HOST, DB_USER, DB_PASS, DB_NAME, USERNAME, PASSWORD, CHEESE, PARM);

		std::cout << "create request" << std::endl;
		http::Request request{ "http://127.0.0.1/index.php" };

		// send a post request
		std::cout << "send request" << std::endl;
		const auto response = request.send
		(
			"POST",
			connection_buffer,
			{
				"Content-Type: application/x-www-form-urlencoded",
				"User-Agent: runscope/0.1",
				"Accept: */*"
			}, std::chrono::seconds(2)
		);

		// Print response to console.
		std::cout << "Response:" << std::endl;
		std::cout << std::string{ response.body.begin(), response.body.end() } << std::endl;
	}

	catch (const std::exception& e)
	{
		std::cerr << "Request failed, error: " << e.what() << std::endl;
	}
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
