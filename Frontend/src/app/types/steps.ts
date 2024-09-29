export type TFormState = {
    currentStep: number;
    totalSteps: number;
    sameDataAsResponse: boolean;
    nanoId: string | null;
    formData: TFormData
    error: string;
    messages: any[];
};

export type TFormData = {
    // Step 1
    date_of_action: string | null; // DateOnly in format YYYY-MM-DD
    office_name: string | null;
    entity_submitting_action: 1 | 2 | 3 | 4 | 5 | null;
    taxpayer_type: 'individual' | 'company' | null;
    taxpayer_data: TIndividualTaxpayer | TCompanyTaxpayer | null;

    // Step 2
    address: {
        country: string | null;
        province: string | null;
        county: string | null;
        municipality: string;
        street?: string | null;
        house_number: string | null;
        apartment_number?: string | null;
        city: string | null;
        postal_code: string | null;
    };

    // Step 3
    action_description: string | null;
    amount: number | null;
};

export type TIndividualTaxpayer = {
    first_name: string | null;
    last_name: string | null;
    pesel: string | null;
    date_of_birth: string | null; // DateOnly in format YYYY-MM-DD
};

export type TCompanyTaxpayer = {
    full_name: string | null;
    short_name: string | null;
    nip: string | null;
};

export type HandleChangeFunction = (name: string, value: any) => void;

export type TStepStart = {
    state: TFormState
    handleChange: HandleChangeFunction
    handleNextStep: () => void
}
export type TStepMid = {
    state: TFormState
    handleChange: HandleChangeFunction
    handleNextStep: () => void
    handlePreviousStep: () => void
}

export type TStepFinal = {
    state: TFormState
    handleChange: HandleChangeFunction
    handlePreviousStep: () => void
}



