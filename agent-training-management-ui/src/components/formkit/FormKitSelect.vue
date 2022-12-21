<template>
	<div
		:id="(($attrs.id ?? props.context.id) as string)"
		ref="selectEl"
		class="a-select"
		:disabled="context.disabled"
	>
		<input
			v-model="inputValue"
			type="hidden"
			:disabled="context.disabled"
			:title="formkitNode.name"
			:name="formkitNode.name"
			:="context.attrs"
		/>

		<a-tooltip
			triggers="none"
			placement="bottom-start"
			theme="formkit-a-select"
			:shown="shown"
		>
			<div class="a-select-container">
				<input
					v-model="displayValue"
					class="formkit-input a-select__input"
					form="nosubmit"
					:disabled="context.disabled"
					:type="context.searchable ? 'text' : 'button'"
					:placeholder="context.placeholder"
					:title="formkitNode.name"
					:name="formkitNode.name"
					:="context.attrs"
					@focus="showDropndown"
					@keydown="handleKeysPress"
				/>

				<div
					v-if="context.showIcon"
					class="a-select__icon"
					@click="toggleDropndown"
				>
					<chevron-down-icon
						class="a-select__arrow-icon"
						:class="[{ '-rotate-180': shown }]"
					/>
				</div>
			</div>

			<template #tooltip>
				<div
					ref="selectDropdownEl"
					class="formkit-dropdown a-select__dropdown"
				>
					<template v-if="inputOptions?.length">
						<a-button
							v-for="(item, index) in inputOptions"
							:key="index"
							:tabindex="index"
							:data-value="item.value"
							:data-selected="selectedOptionIndex == index"
							@click="handleOptionClick"
						>
							{{ item.text }}
						</a-button>
					</template>
					<a-button v-else disabled content="No results" />
				</div>
			</template>
		</a-tooltip>
	</div>
</template>

<script setup lang="ts">
import type { FormKitNode } from '@formkit/core';
import { computedWithControl, useFocusWithin } from '@vueuse/core';
import { computed, ref, watch } from 'vue';
import { ChevronDownIcon } from 'vue-tabler-icons';

import type { FormKitASelect } from '@/types';
import { Kt } from '@/utils';

const props = defineProps<{
	context: GenericObject & FormKitASelect & { node: FormKitNode };
}>();

const selectEl = ref<HTMLElement>();
const selectDropdownEl = ref<HTMLUListElement>();
const { focused } = useFocusWithin(selectEl);
const shown = ref(false);
const selectedOptionIndex = ref<number>();
const inputValue = ref(getInitialInputValue());
const displayValue = ref(getInitialDisplayValue());
const innerTriggered = ref(false);

const formkitNode = computed(() => props.context.node);
const mappedOptions = computed(() => {
	const options = props.context.options;
	let mappedOptions: { value: string; text: string }[];

	if (Array.isArray(options)) {
		mappedOptions = options.map((value) => {
			const v = value.toString();
			return { value: v, text: v };
		});
	} else {
		mappedOptions = Object.keys(options).map((key) => ({
			text: options[key].toString(),
			value: key.toString(),
		}));
	}

	return mappedOptions;
});
const inputOptions = computedWithControl(displayValue, () => {
	const searchValue = displayValue.value;

	if (!props.context.searchable || !searchValue) return mappedOptions.value;

	return mappedOptions.value?.filter((item) =>
		props.context.caseInsensitive
			? item.text.toLowerCase().includes(searchValue.toLowerCase())
			: item.text.includes(searchValue)
	);
});

const showDropndown = () => !shown.value && (shown.value = true);
const hideDropndown = () => shown.value && (shown.value = false);
const toggleDropndown = () => (shown.value = !shown.value);
const handleOptionClick = (e: Event) => {
	new Kt(e.target as HTMLElement)
		.takeIf((opt) => !!opt?.dataset.value)
		?.also((opt) => {
			appendInputs({ value: opt.dataset.value! });

			if (!focused.value && shown.value) {
				hideDropndown();
			}
		});
};
const handleKeysPress = (e: KeyboardEvent) => {
	if (e.key == 'Escape') return hideDropndown();
	else showDropndown();

	const captureKeys = ['Enter', 'Tab', 'ArrowUp', 'ArrowDown', 'j', 'k'];

	if (!captureKeys.includes(e.key) || !selectDropdownEl.value?.children)
		return;

	if (['Tab', 'Enter'].includes(e.key) && inputOptions.value.length) {
		const chooseIndex = selectedOptionIndex.value ?? 0;
		const opt = mappedOptions.value[chooseIndex];

		appendInputs({ value: opt.value, display: opt.text });

		return hideDropndown();
	}

	const optionEls = Array.from(
		selectDropdownEl.value.children
	) as HTMLLIElement[];

	if (['ArrowDown', 'j'].includes(e.key)) {
		const index = selectedOptionIndex.value ?? -1;

		selectedOptionIndex.value = (index + 1) % optionEls.length;
	} else if (['ArrowUp', 'k'].includes(e.key)) {
		let index = selectedOptionIndex.value ?? optionEls.length;

		selectedOptionIndex.value = !index ? optionEls.length - 1 : --index;
	}

	optionEls[selectedOptionIndex.value ?? 0]?.scrollIntoView({
		behavior: 'smooth',
		block: 'nearest',
		inline: 'start',
	});
};

function getInitialInputValue() {
	const value = props.context.value?.toString();

	if (!value) return;

	const options = props.context.options;

	return Array.isArray(options)
		? options.find((o) => o == value)
		: options[value];
}
function getInitialDisplayValue() {
	const options = props.context.options;
	const input = inputValue.value;

	if (props.context.tagMode || !options || !input) return undefined;

	const value = Array.isArray(options)
		? options[options.findIndex((v) => v == input)]
		: options[input as string];

	return appendAffixes(value);
}

function appendInputs(
	dv: { display?: string; value: string },
	triggerOnInputEvent = true
) {
	innerTriggered.value = triggerOnInputEvent;

	if (innerTriggered.value) formkitNode.value.input(dv.value);

	inputValue.value = dv.value;

	const text =
		dv.display ??
		mappedOptions.value.find((mo) => mo.value == dv.value)?.text;

	if (!text) return;

	displayValue.value =
		!props.context.searchable && props.context.inputDisplayAffixes
			? appendAffixes(text)
			: text;
}
function appendAffixes(v?: string) {
	if (!props.context.inputDisplayAffixes) return v ?? '';

	const affix = props.context.inputDisplayAffixes;

	return [affix.left, v, affix.right].filter((v) => v != undefined).join('')!;
}

watch(
	() => props.context.value,
	() => {
		if (innerTriggered.value) {
			innerTriggered.value = false;

			return;
		}

		const cxtValue = props.context.value?.toString();

		if (cxtValue) {
			appendInputs({ value: cxtValue }, false);
		}
	},
	{ immediate: true, flush: 'post' }
);
watch(focused, async (fcs) => {
	if (fcs) return showDropndown();

	shown.value && (await new Promise((r) => setTimeout(r, 100)));
	hideDropndown();
});
</script>

<style lang="scss">
/* prettier-ignore */
@mixin _configs {
	%dropdown-bdr        { @apply border-main-quaternary/50; }
	%option-bg-selected  { @apply bg-main-quaternary; }
}
@include _configs;

.a-select {
	@apply wh-full;

	&-container {
		@apply flex items-center;
	}

	&__input {
		@apply px-4 text-main-senary placeholder-main-quinary/70 wh-[inherit] focus:border-main-primary;

		&[type='button'] {
			@apply flex justify-start;
		}
	}

	&__arrow-icon {
		@apply m-[10px] transform text-main-senary transition wh-[20px];
	}

	&__dropdown {
		@apply list-none py-1;

		button {
			@apply block w-full cursor-pointer select-none rounded-none bg-transparent px-5 py-2 text-start;

			&:hover,
			&[data-selected='true'] {
				@extend%option-bg-selected;
			}

			&[disabled='true'] {
				@apply cursor-default select-text hover:bg-transparent;
			}
		}
	}
}

.v-popper--theme-formkit-a-select {
	.v-popper__inner {
		@extend%dropdown-bdr;
		@include apply-scrollbar(#{&}, y, $auto-hide: false);
		@apply max-h-48 rounded-md border border-main-quaternary/50 bg-main-secondary md:max-h-[300px] xl:max-h-[500px];
	}
	.v-popper__arrow-outer,
	.v-popper__arrow-inner {
		@apply border-transparent;
	}
}
</style>
