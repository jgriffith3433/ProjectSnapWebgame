var LibraryReactUI = {

	FireEvent: function (event)
	{
		var message = Pointer_stringify(event);
		EngineTrigger.dispatch("engine_event", JSON.parse(message));
	},

    DebugLog: function (log)
    {
        var message = Pointer_stringify(log);
		EngineTrigger.dispatch("engine_log", message);
    },
};

mergeInto(LibraryManager.library, LibraryReactUI);