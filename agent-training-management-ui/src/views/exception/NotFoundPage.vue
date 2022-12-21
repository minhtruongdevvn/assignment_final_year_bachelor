<template>
	<body class="overflow-hidden">
		<div
			class="flex flex-col items-center justify-center gap-12 py-28 px-6 md:px-16 md:py-20 lg:mx-40 lg:flex-row lg:gap-20 lg:py-40"
		>
			<div class="w-full lg:w-1/2">
				<img
					class="hidden lg:block"
					src="https://i.ibb.co/v30JLYr/Group-192-2.png"
				/>
				<img
					class="hidden md:block lg:hidden"
					src="https://i.ibb.co/c1ggfn2/Group-193.png"
				/>
				<img
					class="md:hidden"
					src="https://i.ibb.co/8gTVH2Y/Group-198.png"
				/>
			</div>
			<div class="w-full lg:w-1/2">
				<h1
					class="py-4 text-center text-3xl font-extrabold text-main-quinary md:text-left lg:text-4xl"
				>
					Looks like you've found the doorway to the great nothing
				</h1>
				<p class="py-4 text-base text-main-quinary">
					The content you're looking for doesn't exist. Either it was
					removed, or you mistyped the link.
				</p>
				<p class="py-2 text-base text-main-quinary">
					Sorry about that! Please visit our hompage to get where you
					need to go.
				</p>
				<a-button
					tag="router-link"
					to="/"
					class="my-4 w-full rounded-md border bg-indigo-600 px-1 py-5 text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-700 focus:ring-opacity-50 sm:px-16 lg:w-auto"
					content="Go back to Homepage"
				/>
			</div>
		</div>
	</body>
</template>
