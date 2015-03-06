--Object orientation example
class = require 'middleclass'

objects = {}

function love.load()
	local obj = require('Object0')
	table.insert(objects,obj(0,0,2))
	obj.i = 8
end

function love.keypressed(key)
	if key == " " then
		local obj = require('Object0')
		table.insert(objects,obj(0,table.getn(objects)*48,2))
		obj.i = 8
	elseif key == "e" then
		local obj = require('Object1')
		table.insert(objects,obj(0,table.getn(objects)*48,2))
		obj.i = 5
		obj.k = 9
	elseif key == "r" then
		table.remove(objects,table.getn(objects))
	end
end

function love.update()
	for i = 1, table.getn(objects) do
		if objects[i].tp == "Object0" then
			objects[i].i = objects[i].i+1
		end
	end
end

function love.draw()
	love.graphics.print("Object Orientation example",164,0)
	love.graphics.print("Object Count: " .. table.getn(objects),164,16)
	love.graphics.print("Press space to add objects of type " .. '"' .. "object0" .. '"',164,32)
	love.graphics.print("Press e to add objects of type " .. '"' .. "object1" .. '"',164,48)
	love.graphics.print("Press r to remove objects",164,64)
	
	for i = 1, table.getn(objects) do
		objects[i]:draw()
	end
end