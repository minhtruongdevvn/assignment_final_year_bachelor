export {};

String.prototype.toCamelCase = function (replaceWhiteSpaces = false) {
	const camelStr = this.replace(/(?:^\w|[A-Z]|-|\b\w)/g, (ltr, idx) =>
		idx === 0 ? ltr.toLowerCase() : ltr.toUpperCase()
	);
	return replaceWhiteSpaces ? camelStr.replace(/\s+|-/g, '') : camelStr;
};

String.prototype.toPascalCase = function (replaceWhiteSpaces = false) {
	const pascalStr = this.toLowerCase()
		.split(' ')
		.map((str) => str.charAt(0).toUpperCase() + str.slice(1))
		.join(' ');
	return replaceWhiteSpaces ? pascalStr.replace(/\s+|-/g, '') : pascalStr;
};
