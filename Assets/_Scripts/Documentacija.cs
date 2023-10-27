
//Pick - up objekti trebaju imati tag "Objective" i skriptu "PickUpObject"
//Objekti na koje utječe gravitacija trebaju imati ConstantForce omponentu  i skriptu "GravityDirection"
//Objekti koji se smanjuju imaju u skripti "SizeChange" imaju mogućnost da postanu pick-up objekti
//Objekti s "TimeRewind" pamte poziciju kada se pomiću ( !Rigidbody.IsSleeping() ) i kada se aktivira TimeRewind vraćaju se na početnu poziciju 
//i iz nekog razloga se Player ljepi za strop

//stavi na Player-a "InputManager","PickUp","PlayerMovement"
//I stavi Transform za Cinemachine
//na Cinemachine Virtual cameru stavi "CinemachineFOVExtension"
//PickUp treba Transform za HeldArea