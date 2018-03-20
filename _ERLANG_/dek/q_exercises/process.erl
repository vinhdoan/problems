%% --------------------------------------------------------------------------------
%% PART 3 : PROCESSES
%% --------------------------------------------------------------------------------

%% 01. start_ring(N, M) -> ok.
%%     Write a function which starts N processes in a ring,
%%     and sends a message M times around all the processes in the ring.
%%     After the messages have been sent the processes should terminate gracefully.
%%     Messages sent in a ring of processes.
%%     Example: start_ring(2, 2).
%%              -> create process 1: <0.210.0>
%%              -> create process 2: <0.211.0>
%%              : process 1 --> send message 3
%%              : process 2 --> send message 3
%%              [<0.210.0>,<0.211.0>]
%%              : process 1 --> send message 2
%%              : process 2 --> send message 2
%%              : process 1 --> send message 1
%%              : process 2 --> send message 1
%%              * process 1 terminated!
%%              * process 2 terminated!

%% 02. Write a process that receives strings representing mathematical expressions
%%     and sends their value back to the sender.
%%     Now write a process that reads strings from the console, sends it to the
%%     process and prints the value on the console.
%%     The strint "-q" shall exit the program.
%%     Example: math() ->
%%                  *** start math application ***
%%                      math expression: 1 + 1 - 1
%%                      result: 1
%%                      math expression: 2 + 2 * 2 + 2 / 2
%%                      result: 7.0
%%                      math expression: -q
%%                      result: "server is terminated!"
%%                      *** stop math application ***

%% 03. Using gen_server behavior of erlang for this exercise.
%%     Create module election_server.erl.
%%     Write function start(N) that starts n processes in a ring topology and that
%%     assigns to each process in that ring some random but unique integer ID.
%%     Implement the following election algorithm:
%%     * Let each process send its ID "clockwise" around the ring.
%%     * A process that receives an ID larger than its own ID lost the election and
%%       just passes received IDs around the ring.
%%     * The process that receives its own ID wins the election
%%     Example: election_server:start(5) ->
%%              ID: [196,31,107,355,382]
%%              election_server:info() ->
%%              *** process: 196, next process: election_server_31
%%              *** process: 31, next process: election_server_107
%%              *** process: 107, next process: election_server_355
%%              *** process: 355, next process: election_server_382
%%              *** process: 382, next process: election_server_196
%%              election_server:election() ->
%%              *** process 382: win the election!
%%              election_server:stop() ->
%%              *** process election_server_196: terminated!
%%              *** process election_server_31: terminated!
%%              *** process election_server_107: terminated!
%%              *** process election_server_355: terminated!
%%              *** process election_server_382: terminated!

%% 04. Using gen_statem behavior of erlang for this exercise.
%%     Write a function that starts n processes in a ring topology and that assigns
%%     to each process in that ring some random but unique integer ID.
%%     Implement the following peterson algorithm:
%%     * A process is in two states: It is either participating in the election
%%       (active mode) or forwarding messages (called the relay mode).
%%       At the beginning, all processes have active mode.
%%     * Each process send its ID "clockwise" arround the ring.
%%     * Let say process i has iID, it must receive 2 meesages from i-1 (i-1ID) and
%%       i-2 (i-2ID) process.
%%     * If i-1ID is the highest PID, then process i changes its ID to i-1ID and
%%       send i-1ID to next node.
%%     * If not, process i switchs to relay mode and only forwarding message.
%%     * The process that receives its own ID wins the election.
%%     Example: election_statem:start(5) ->
%%              ID: [411,430,90,210,358]
%%              election_statem:info() ->
%%              *** process id: 411, next node is: election_statem_430
%%              *** process id: 430, next node is: election_statem_90
%%              *** process id: 90, next node is: election_statem_210
%%              *** process id: 210, next node is: election_statem_358
%%              *** process id: 358, next node is: election_statem_411
%%              election_statem:election() ->
%%              *** process 90: update virtual_id: 430
%%              *** process 210: change to relay mode
%%              *** process 358: change to relay mode
%%              *** process 411: change to relay mode
%%              *** process 430: change to relay mode
%%              *** processs 90 (430): win the election!
%%              election_statem:stop() ->
%%              *** process election_statem_411 terminated!
%%              *** process election_statem_430 terminated!
%%              *** process election_statem_90 terminated!
%%              *** process election_statem_210 terminated!
%%              *** process election_statem_358 terminated!
