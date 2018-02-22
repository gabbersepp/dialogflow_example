<?php
function processMessage($update) {
    if($update["result"]["action"] == "math.calculate"){
		
		 $req_dump = print_r($update, TRUE);
$fp = fopen('request.log', 'a');
fwrite($fp, $req_dump);
fclose($fp);

		if ($update["result"]["parameters"]["math_binary_operand_right"] == "0" && $update["result"]["parameters"]["math_operator_binary_operator"] == "/") {
			$obj = new stdClass();
			$obj->name = "expecting_math_request_binary_right_operand";
			$obj->lifespan  = 1;
			$obj->parameters = $update["result"]["contexts"][0]["parameters"]; // execute_math_calc context
			$obj2 = new stdClass();
			$obj2->name="execute_math_calc";
			$obj2->lifespan = 0; // clean this context
			sendMessage(array(
				"source" => $update["result"]["source"],
				"speech" => "Diese Operation ist nicht erlaubt. Bitte geben Sie einen korrekten rechten Operanden an.",
				"displayText" => "Diese Operation ist nicht erlaubt. Bitte geben Sie einen korrekten rechten Operanden an.",
				"contextOut" => array(0 => $obj, 1 => $obj2) 
			));
		} else {
			/*sendMessage(array(
				"source" => $update["result"]["source"],
				"speech" => "Hello from webhook",
				"displayText" => "Hello from webhook",
				"contextOut" => array()
			));*/
		}

    }
}
 
function sendMessage($parameters) {
    echo json_encode($parameters);
}
 
$update_response = file_get_contents("php://input");
$update = json_decode($update_response, true);
if (isset($update["result"]["action"])) {
    processMessage($update);
}