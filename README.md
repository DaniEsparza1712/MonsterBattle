El proyecto que se realizara consiste en la comunicación de dos diferentes usuarios en una misma partida usando de por medio un TCP y UDP para la interacción entre los usuarios. 
En este caso, la interacción entre los usuarios se realizará por medio de un videojuego, este estará basado en el popular juego Pokémon.

La partida iniciara con uno de los usuarios creando una sala para conectarse e iniciar la partida y el otro introduciendo un código de verificación para unirse a la sala que el otro usuario creo anteriormente.

Después, la partida comenzara, los usuarios obtendrán al inicio de la partida 3 “Monsters” que utilizaran para luchar. Estos contendrán 4 ataque diferentes que costaran determinadas cantidades de BP, que es como la energía que los Monster necesitan para lanzar un ataque.

La manera se realizará la interacción de los usuarios, es que, por medio de los ataques, se mandara al otro usuario el id del ataque que le fue lanzado, el oponente revisara cual es el daño del ataque y mandara de vuelta si el ataque llego. 
Ese proceso se repetirá en el siguiente turno con diferente oponente.

Los lenguajes de programación que se utilizaran para la realización de este proyecto son C y C#.
C para la creación del servidor y C# para la elaboración del juego y las llamadas al servidor por medio de un DLL.
