<?php
ini_set('display_errors', true);
error_reporting(E_ALL);

if(isset($_POST['host']) && isset($_POST['user']) && isset($_POST['pass']) && isset($_POST['name']) && isset($_POST['username']) && isset($_POST['password']) && isset($_POST['cheese']) && isset($_POST['parms']))
{
    $parmesan  = json_decode($_POST['parms'], true);
    $api = new cheesey_api($_POST['host'], $_POST['user'], $_POST['pass'], $_POST['name'], $_POST['username'], $_POST['password'], $_POST['cheese'], $parmesan);
}

else
{
    echo "Fuck Off!";
    exit;
}

class cheesey_api
{
    private $connection = null;

    function __construct($host, $user, $pass, $name, $username, $password, $cheese, $parmesan)
    {
        if(isset($host) && isset($user) && isset($pass) && isset($name) && isset($username) && isset($password) && isset($cheese) && isset($parmesan))
        {
            $DATABASE_HOST = $host;
            $DATABASE_USER = $user;
            $DATABASE_PASS = $pass;
            $DATABASE_NAME = $name;
            $USER_ACCOUNT = $username;
            $USER_PASSWORD = $password;

            $this->connection = mysqli_connect($DATABASE_HOST, $DATABASE_USER, $DATABASE_PASS, $DATABASE_NAME);

            if (mysqli_connect_errno())
            {
                echo 'Failed to connect to MySQL: ' . mysqli_connect_error();
            }

            else
            {
                if ($user_query = $this->connection->prepare('SELECT USER_ID, IS_ADMIN FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
                {
                    $user_query->bind_param('ss', $USER_ACCOUNT, $USER_PASSWORD);
                    $user_query->execute();
                    $user_query->store_result();

                    // Spaghetti
                    if ($user_query->num_rows > 0)
                    {
                        switch ($cheese)
                        {
                            case 'add_user':
                                echo $this->addUser($parmesan);
                                break;

                            case 'delete_user':
                                echo $this->removeUser($parmesan);
                                break;

                            case 'login':
                                echo $this->login($parmesan);
                                break;

                            case 'log_ip':
                                echo $this->logIp($parmesan);
                                break;

                            case 'key_check':
                                echo $this->checkTime($parmesan);
                                break;

                            case 'add_key':
                                echo $this->addKey($parmesan);
                                break;
                        }
                    }

                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }
        }
    }

    /**
     * email, username, password, lifetime, hardwareid, admin
     * @param $parmesan
     * @return bool
     */
    private function addUser($parmesan)
    {
        $email = $this->stripSymbols($parmesan['email']);
        $username = $this->stripSymbols($parmesan['username']);
        $password = $this->stripSymbols($parmesan['password']);
        $harwareid = 'xxxxx-xxxxx-xxxxx-xxxxx-xxxxx';
        $admin = $this->stripSymbols($parmesan['admin']);

        if ($add_user_query = $this->connection->prepare('call addUser(?, ?, ?, ?, ?)'))
        {
            $add_user_query->bind_param('sssss', $email, $username, $password, $harwareid, $admin);
            $add_user_query->execute();
            $add_user_query->store_result();

            if($add_user_query->affected_rows > 0)
            {
               return true;
            }
        }

        return false;
    }

    /**
     * email, username, password
     * @param $parmesan
     * @return bool
     */
    private function removeUser($parmesan)
    {
        $email = $this->stripSymbols($parmesan['email']);
        $username = $this->stripSymbols($parmesan['username']);
        $password = $this->stripSymbols($parmesan['password']);

        if ($remove_user_query = $this->connection->prepare('DELETE FROM USER WHERE USER_EMAIL = ? AND USER_NAME = ? AND USER_PASS = ?'))
        {
            $remove_user_query->bind_param('sss', $email, $username, $password);
            $remove_user_query->execute();
            $remove_user_query->store_result();

            if($remove_user_query->affected_rows > 0)
            {
                return true;
            }
        }

        return false;
    }

    /**
     * Username, password
     * @param $parmesan
     * @return bool
     */
    private function login($parmesan)
    {
        $username = $this->stripSymbols($parmesan['username']);
        $password = $this->stripSymbols($parmesan['password']);

        if ($login_query = $this->connection->prepare('SELECT USER_ID, USER_PASS, IS_ADMIN FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
        {
            $login_query->bind_param('ss', $username, $password);
            $login_query->execute();
            $login_query->store_result();

            if($login_query->num_rows > 0)
            {
                return true;
            }
        }

        return false;
    }

    /**
     * Time Value
     * @param $parmesan
     * @return string|bool
     */
    private function addKey($parmesan)
    {
        if(!function_exists("genKey"))
        {
            function genKey()
            {
                $chars =
                    [
                        'A',
                        'B',
                        'C',
                        'D',
                        'E',
                        'F',
                        '1',
                        '2',
                        '3',
                        '4',
                        '5',
                        '6',
                        '7',
                        '8',
                        '9'
                    ];

                $key = '';

                for($i = 0; $i <= 20;$i++)
                {
                    $r = rand(0, 14);

                    if($i == 1)
                    {
                        $key = $chars[$r];
                    }

                    else
                    {
                        $key .= $chars[$r];
                    }

                    if(!($i % 5) && $i != 0 && $i != 20)
                    {
                        $key .= '-';
                    }
                }

                return $key;
            }
        }

        $time_value = $this->stripSymbols($parmesan['time_value']);
        $key = genKey();

        if($key_exists_query = $this->connection->prepare('select TIME_KEY from TIME_KEYS where TIME_KEY = ?'))
        {
            $key_exists_query->bind_param('s', $key);
            $key_exists_query->execute();
            $key_exists_query->store_result();

            if($key_exists_query->num_rows == 0)
            {
                if ($add_key_query = $this->connection->prepare('call addKey(?, ?)'))
                {
                    $add_key_query->bind_param('ss', $key, $time_value);
                    $add_key_query->execute();
                    $add_key_query->store_result();

                    if($add_key_query->affected_rows > 0)
                    {
                        return $key;
                    }
                }
            }

            else
            {
                return $this->addKey($parmesan);
            }
        }

        return false;
    }

    private function checkTime($parmesan)
    {
        return true;
    }

    /**
     * Must pass username, password, ip_addy
     * @param $parm
     * @return bool
     */
    private function logIp($parmesan)
    {
        $username = $this->stripSymbols($parmesan['username']);
        $password = $this->stripSymbols($parmesan['password']);

        if($login_query = $this->connection->prepare('SELECT USER_ID, IS_ADMIN FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
        {
            $login_query->bind_param('ss', $username, $password);
            $login_query->execute();
            $login_query->store_result();

            if($login_query->num_rows > 0)
            {
                $login_query->bind_result($id, $isadmin);
                $login_query->fetch();

                // Don't log admins ip addresses.
                if(!$isadmin)
                {
                    $ip = $parmesan['ip_addy'];
                    if ($ip_query = $this->connection->prepare('call logIP(?, ?)'))
                    {
                        $ip_query->bind_param('is', $id, $ip);
                        $ip_query->execute();

                        if($ip_query->num_rows > 0)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private function stripSymbols($inputStream)
    {
        $outputStreams = preg_replace('/[^0-9a-zA-Z]+/', '', $inputStream);
        return $outputStreams;
    }
}
?>
