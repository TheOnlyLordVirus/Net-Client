<?php
if(isset($_POST['tool']))
{
    if($_POST['tool'] == 'version')
    {
        echo '1.0.1.0';
    }
}
?>