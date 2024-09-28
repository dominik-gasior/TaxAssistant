export type TFormState = {
    currentStep: number;
    totalSteps: number;
    sameDataAsResponse: boolean;
    formData: TFormData
};

export type TFormData = {
    // Step 1
    date_of_action: string; // DateOnly in format YYYY-MM-DD
    office_name: string;
    entity_submitting_action: 1 | 2 | 3 | 4 | 5;
    taxpayer_type: 'individual' | 'company';
    taxpayer_data: TIndividualTaxpayer | TCompanyTaxpayer;

    // Step 2
    address: {
        country: string;
        province: string;
        county: string;
        municipality: string;
        street?: string;
        house_number: string;
        apartment_number?: string;
        city: string;
        postal_code: string;
    };

    // Step 3
    action_description: string;
    amount: number;
};

export type TIndividualTaxpayer = {
    first_name: string;
    last_name: string;
    pesel: string;
    date_of_birth: string; // DateOnly in format YYYY-MM-DD
};

export type TCompanyTaxpayer = {
    full_name: string;
    short_name: string;
    nip: string;
};

export type HandleChangeFunction = (name: string, value: any) => void;

export type TStepStart = {
    state: TFormState
    handleChange: HandleChangeFunction
    handleNextStep: () => void
    onSave: () => void
}
export type TStepMid = {
    state: TFormState
    handleChange: HandleChangeFunction
    handleNextStep: () => void
    handlePreviousStep: () => void
    onSave: () => void
}

export type TStepFinal = {
    state: TFormState
    handleChange: HandleChangeFunction
    handlePreviousStep: () => void
    onSave: () => void
}



