function Character:new()
	local char = {}
	setmetatable(char, self)
	self.__index = self
	-- Initialize character properties
	char.name = "Unnamed"
	char.sprite = function(sprite) return love.graphics.newImage(sprite) end
	char.position = {x = 0, y = 0}
	char.size = {width = 0, height = 0}
	char.body = function (world, type) return love.physics.newBody(world, char.position.x, char.position.y, type) end
	char.shape = function() return love.physics.newRectangleShape(char.size.width / 2, char.size.height / 2, char.size.width, char.size.height) end
	char.fixture = function() return love.physics.newFixture(char.body(), char.shape()) end
	return char	
end
