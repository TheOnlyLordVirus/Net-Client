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
#include "HTTPRequest.hpp"

// New
#define PORT 80

// Old
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


/**
 * @brief Pass the arguments to the DB.
 * 
 * @param args POST_VALUES, DB_HOST, DB_USER, DB_PASS, DB_NAME, USERNAME, PASSWORD, CHEESE, PARM
 */
void apiAssistant(char* args[])
{
	try
	{
		std::cout << "try post" << std::endl;

		char connection_buffer[sizeof(args)];
		memset(connection_buffer, 0, 0xFFFF);
		sprintf(connection_buffer, /*POST_VALUES*/args[0], /*DB_HOST*/args[1], /*DB_USER*/args[2], /*DB_PASS*/args[3], /*DB_NAME*/args[4], /*USERNAME*/args[5], /*PASSWORD*/args[6], /*CHEESE*/args[7], /*PARM*/args[8]);

		std::cout << "create request" << std::endl;
		http::Request request{ "http://127.0.0.1/index.php" };

		// send a post request
		std::cout << "send request" << std::endl;
		const char* response = request.send
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

/**
 * @brief 
 */
char* socketListener()
{
	int sockfd, newsockfd;
	socklen_t client_length;
	char buffer[256];
	struct sockaddr_in server_address, client_address;
	int n;

	// create a socket
	// socket(int domain, int type, int protocol)
	sockfd =  socket(AF_INET, SOCK_STREAM, 0);
	if (sockfd < 0) 
	{
		std::cout << "ERROR opening socket" << std::endl;
	}

	// clear address structure
	memset((char *) &server_address, 0, sizeof(server_address));

	/* setup the host_addr structure for use in bind call */
	// server byte order
	server_address.sin_family = AF_INET;  

	// automatically be filled with current host's IP address
	server_address.sin_addr.s_addr = INADDR_ANY;  

	// convert short integer value for port must be converted into network byte order
	server_address.sin_port = htons(PORT);

	// bind(int fd, struct sockaddr *local_addr, socklen_t addr_length)
	// bind() passes file descriptor, the address structure, 
	// and the length of the address structure
	// This bind() call will bind  the socket to the current IP address on port, PORT
	if (bind(sockfd, (struct sockaddr *) &server_address, sizeof(server_address)) < 0)
	{
		std::cout << "ERROR opening socket" << std::endl;
	}

	// This listen() call tells the socket to listen to the incoming connections.
	// The listen() function places all incoming connection into a backlog queue
	// until accept() call accepts the connection.
	// Here, we set the maximum size for the backlog queue to 5.
	listen(sockfd, 5);

	// The accept() call actually accepts an incoming connection
	client_length = sizeof(client_address);

	// This accept() function will write the connecting client's address info 
	// into the the address structure and the size of that structure is clilen.
	// The accept() returns a new socket file descriptor for the accepted connection.
	// So, the original socket file descriptor can continue to be used 
	// for accepting new connections while the new socker file descriptor is used for
	// communicating with the connected client.
	newsockfd = accept(sockfd, (struct sockaddr *) &client_address, &client_length);
	if (newsockfd < 0)
	{
		std::cout << "ERROR opening socket" << std::endl;
	}

	printf("Server: got connection from %s port %d\n",inet_ntoa(cli_addr.sin_addr), ntohs(cli_addr.sin_port));


	// This send() function sends the 13 bytes of the string to the new socket
	send(newsockfd, "Command Recieved!\n", 13, 0);

	memset(buffer, 0, 256);

	n = read(newsockfd, buffer, 255);
	if (n < 0)
	{
		std::cout << "ERROR opening socket" << std::endl;
	}

	return buffer;
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
	// Test custom input
	if(argc > 0)
	{
		apiAssistant(args);

		std::string inputbuffer = "";
		std::cin >> inputbuffer;
		return 0;
	}


	// Call socket listenr, when input buffer is returned from packet data, we then will send the buffer params to apiAssistant 
	else
	{
		char* client_input = socketListener();
		
		apiAssistant(client_input);

		std::string inputbuffer = "";
		std::cin >> inputbuffer;
		return 0;
	}
}
