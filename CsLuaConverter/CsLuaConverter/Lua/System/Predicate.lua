﻿System.Predicate = _M.NE({["#"] = function(interactionElement, generics, staticValues)
    local typeObject = System.Type('Predicate','System',System.Object.__typeof,#(generics),generics,nil,interactionElement,'Class',10325);
    local level = 2;
    local members = {
        
    };

    _M.IM(members,'Invoke',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = generics,
        numMethodGenerics = 0,
        signatureHash = _M.SH(unpack(generics)),
        returnType = System.Boolean.__typeof,
        func = function(element,...)
            return (element[typeObject.level].innerAction % _M.DOT)(...);
        end,
    });

    local constructors = {
        {
            types = {typeObject},
            func = function(element, innerAction) 
                element[typeObject.level].innerAction = innerAction;
            end,
        },
        {
            types = {Lua.Function.__typeof},
            func = function(element, innerAction) 
                element[typeObject.level].innerAction = innerAction;
            end,
        }
    };

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end
    return "Class", typeObject, members, constructors, objectGenerator;
end})