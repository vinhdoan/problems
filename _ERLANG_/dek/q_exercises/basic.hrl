%% -------------------------------------------------------------------------------------
%% MACROS FOR ERRORS
%% -------------------------------------------------------------------------------------
-define(arg_list, arg_must_be_a_list).
-define(arg_pos_int, arg_must_be_a_pos_int).
-define(element_pos_int, element_must_be_an_pos_int).
-define(element_int, element_must_be_an_int).

-define(collatz_error, ?arg_pos_int).
-define(element_reverse_error, ?arg_list).
-define(remove_duplicate_error, ?arg_list).
-define(skeleton_error, ?arg_list).
-define(eliminate_error, second_arg_must_be_a_list).
-define(quicksort_error, ?arg_list).
-define(sqrt_error, arg_must_be_a_non_neg_int).
-define(zap_gremlins_error, ?arg_list).
-define(rot_13_error, ?arg_list).
-define(tree_arg_error, ?arg_list).
-define(tree_element_error, ?element_int).
-define(neighbor_merge_arg_error, ?arg_list).
-define(neighbor_merge_element_error, element_must_be_a_tuple_of_2_ascending_int).
-define(percent_arg_error, ?arg_list).
-define(percent_element_error, ?element_pos_int).

%% -------------------------------------------------------------------------------------
%% OTHER MACROS
%% -------------------------------------------------------------------------------------
-define(is_even(X), N band 1 == 0).
-define(is_pos_integer(X), is_integer(X) andalso X > 0).
-define(is_uppercase(H), H >= $A andalso H =< $Z).
-define(is_lowercase(H), H >= $a andalso H =< $z).
