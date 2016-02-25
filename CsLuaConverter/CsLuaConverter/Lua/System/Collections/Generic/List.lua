﻿System.Collections.Generic.List = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IList.__typeof,
        System.Collections.Generic.IList[generics].__typeof,
        System.Collections.ICollection.__typeof,
        System.Collections.Generic.ICollection[generics].__typeof,
        System.Collections.IEnumerable.__typeof,
        System.Collections.Generic.IEnumerable[generics].__typeof,
        System.Collections.Generic.IReadOnlyList[generics].__typeof,
        System.Collections.Generic.IReadOnlyCollection[generics].__typeof,
    };
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('List','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement);

    local getCount = function(element)
        return not(element[typeObject.level][0] == nil) and (#(element[typeObject.level]) + 1) or 0;
    end

    _M.IM(members,'ForEach',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {System.Action[{generics[1]}].__typeof},
        func = function(element,action)
            for i = 0,getCount(element)-1 do
                (action%_M.DOT)(element[typeObject.level][i]);
            end
        end,
    });

    _M.IM(members,'Count',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return getCount(element);
        end,
    });

    _M.IM(members,'Capacity',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            local c = getCount(element);
            return c == 0 and c or math.max(4, c);
        end,
    });

    local ThrowIfOutOfRange = function(element, index)
        local c = getCount(element);
        if not(type(index) == "number") or index < 0 or index >= c then
            _M.Throw(System.ArgumentOutOfRangeException());
        end
    end

    _M.IM(members,'#',{
        level = typeObject.Level,
        memberType = 'Indexer',
        scope = 'Public',
        types = {generics[1]},
        get = function(element, index)
            ThrowIfOutOfRange(element, index);
            return element[typeObject.level][index];
        end,
        set = function(element, index, value)
            ThrowIfOutOfRange(element, index);
            element[typeObject.level][index] = value;
        end,
    });

    _M.IM(members,'Add',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {generics[1]},
        func = function(element,value)
            element[typeObject.level][getCount(element)] = value;
        end,
    });

    _M.IM(members,'GetEnumerator',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {},
        func = function(element)
            local t = {};
            for i = 0, getCount(element) do
                t[i+1] = element[typeObject.level][i];
            end
            return pairs(t);
        end,
    });

    _M.IM(members,'IsFixedSize',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return false;
        end,
    });

    _M.IM(members,'IsReadOnly',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return false;
        end,
    });

    _M.IM(members,'IsSynchronized',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return false;
        end,
    });

    _M.IM(members,'SyncRoot',{
        level = typeObject.Level,
        memberType = 'Property',
        scope = 'Public',
        types = {},
        get = function(element)
            return System.Object();
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        },
        {
            types = {System.Collections.Generic.IEnumerable[{generics[1]}].__typeof},
            func = function(element, values)
                local c = 0;
                for _,v in (values %_M.DOT).GetEnumerator() do
                    element[typeObject.level][c] = v;
                    c = c + 1;
                end
            end,
        },
    };

    local initialize = function(element, values)
        for i=1,#(values) do
            element[typeObject.level][i-1] = values[i];
        end
    end

    local objectGenerator = function() 
        return {
            [1] = {},
            [2] = {}, 
            ["type"] = typeObject,
            __metaType = _M.MetaTypes.ClassObject,
        }; 
    end

    return "Class", typeObject, members, constructors, objectGenerator, implements, initialize;
end})