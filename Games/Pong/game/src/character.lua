function Character:new()
	local char = {}
	setmetatable(char, self)
	self.__index = self
	-- Initialize character properties
	char.name = "Unnamed"
	char.sprite = function(sprite) return love.graphics.newImage(sprite) end
	char.position = {x = 0, y = 0}
	return char	
end
