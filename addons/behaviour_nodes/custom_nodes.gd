tool
extends EditorPlugin

func _enter_tree():		
	add_custom_type("B_Sequence", "Node", preload("res://Scripts/Behavior Tree/Base/SequenceNode.cs"), null)
	add_custom_type("B_FallBack", "Node", preload("res://Scripts/Behavior Tree/Base/FallbackNode.cs"), null)

func _exit_tree():
	remove_custom_type("B_Sequence")
	remove_custom_type("B_FallBack")

