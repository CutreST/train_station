tool
extends EditorPlugin

func _enter_tree():		
	add_custom_type("B_Sequence", "Node", preload("res://Scripts/Behavior Tree/Base/SequenceNode.cs"), null)
	add_custom_type("B_FallBack", "Node", preload("res://Scripts/Behavior Tree/Base/FallbackNode.cs"), null)
	add_custom_type("B_VN_Dialog", "Node", preload("res://Scripts/Behavior Tree/VN Nodes/DialogNode.cs"), null)
	add_custom_type("B_Tree_Controller", "Node", preload("res://Scripts/Behavior Tree/Base/TreeController.cs"), null)
	add_custom_type("B_VN_Close", "Node", preload("res://Scripts/Behavior Tree/VN Nodes/EndDialog.cs"), null)

func _exit_tree():
	remove_custom_type("B_Sequence")
	remove_custom_type("B_FallBack")
	remove_custom_type("B_VN_Dialog")
	remove_custom_type("B_Tree_Controller")
	remove_custom_type("B_VN_Close")

