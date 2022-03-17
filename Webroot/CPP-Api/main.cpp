#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <iostream>
#include <cstring>
#include <cstdio>
#include <cstdlib>
#include <unistd.h>
#include <sys/types.h> 
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include "HTTPRequest.hpp"

#define PORT 5060

using namespace std;

#pragma warning(disable:4996)

/**
 * @brief Pass the arguments to the DB.
 * 
 * @param args POST_VALUES, DB_HOST, DB_USER, DB_PASS, DB_NAME, USERNAME, PASSWORD, CHEESE, PARM
 */
char* apiAssistant(char* args[])
{
	try
	{
		std::cout << "Api Response:" << std::endl;

		if(sizeof(args) / sizeof(args[0]) == 9)
		{
			char connection_buffer[sizeof(args)];
			memset(connection_buffer, 0, sizeof(args));
			sprintf(connection_buffer, /*POST_VALUES*/args[0], /*DB_HOST*/args[1], /*DB_USER*/args[2], /*DB_PASS*/args[3], /*DB_NAME*/args[4], /*USERNAME*/args[5], /*PASSWORD*/args[6], /*CHEESE*/args[7], /*PARM*/args[8]);
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
			std::string str = std::string{ response.body.begin(), response.body.end() };
			char* c = const_cast<char*>(str.c_str());
			return c;
		}

		else
		{
			// Invalid number of arguments, don't tell the client this for security purposes.
			std::cerr << "Request failed, error!" << std::endl;
		}
	}

	catch (const std::exception& e)
	{
		std::cerr << "Request failed, error: " << e.what() << std::endl;
	}
}

/**
 * @brief Opens a socket and waits for a message.
 */
void socketListener()
{
	int main_socket_file_descriptor, incomming_socket_file_descriptor;
	socklen_t client_length;
	char recieved_client_command[0xFF];
	struct sockaddr_in server_address, client_address;
	int n;

	// create a socket
	// socket(int domain, int type, int protocol)
	main_socket_file_descriptor =  socket(AF_INET, SOCK_STREAM, 0);
	if (main_socket_file_descriptor < 0) 
	{
		std::cout << "ERROR opening socket" << std::endl;
	}

	// clear address structure
	memset((char *) &server_address, 0, sizeof(server_address));
	server_address.sin_family = AF_INET;  
	server_address.sin_addr.s_addr = INADDR_ANY;  
	server_address.sin_port = htons(PORT);

	if (bind(main_socket_file_descriptor, (struct sockaddr *) &server_address, sizeof(server_address)) < 0)
	{
		std::cout << "ERROR binding socket" << std::endl;
	}
	
	// Listen
	listen(main_socket_file_descriptor, 5/*Size of the backlock of incomming connections.*/);
	std::cout << "Socket Open on port: " << PORT << std::endl;

	// The accept() call actually accepts an incoming connection
	client_length = sizeof(client_address);
	incomming_socket_file_descriptor = accept(main_socket_file_descriptor, (struct sockaddr *) &client_address, &client_length);
	if (incomming_socket_file_descriptor < 0)
	{
		std::cout << "ERROR accepting socket" << std::endl;
	}
	printf("Server: got connection from: %s, on port: %d\n", inet_ntoa(client_address.sin_addr), ntohs(client_address.sin_port));

	memset(recieved_client_command, 0, 0xFF);
	n = read(incomming_socket_file_descriptor, recieved_client_command, 0xFF);
	if (n < 0)
	{
		std::cout << "ERROR opening socket" << std::endl;
	}
	std::cout << "(DEBUG) Data Recieved:" << (char*)recieved_client_command << std::endl;

	// Send api result back to client.
	char* api_result = apiAssistant((char**)recieved_client_command);
	if(api_result)
	{
		send(incomming_socket_file_descriptor, (char*)api_result, sizeof(api_result), 0);
	}

	// Error out.
	else
	{
		send(incomming_socket_file_descriptor, "Api Error!", sizeof(11), 0);
	}
}


/**
 * @brief Entry point to the application
 * 
 * @param argc Count of aguments
 * @param args Argument array
 * @return int 
 */
int main(int argc, char* args[])
{
	// Custom API call from Cli.
	if(argc == 9)
	{
		apiAssistant(args);
		std::string inputbuffer = "";
		std::cin >> inputbuffer;
	}

	// Cli parameter error.
	else if(argc > 9)
	{
		std:cerr << "Invalid number of aguments." << std::endl;
	}

	// Call socket listener, when input buffer is returned from packet data, we then will send the buffer params to apiAssistant 
	else
	{
		socketListener();
		/*
		while(1)
		{
			socketListener();
		}
		*/
	}

	return 0;
}
