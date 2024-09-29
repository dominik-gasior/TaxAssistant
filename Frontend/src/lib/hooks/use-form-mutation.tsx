"use client"

import { useMutation } from "@tanstack/react-query"

import { updateForm } from "../action"
import { useForm } from "./use-form"

export const useFormMutation = () => {
  const { state, dispatch } = useForm()
  const mutateForm = useMutation({
    mutationFn: async (nanoId: string) => updateForm(nanoId, state.formData),

    onSuccess: (data) => {
      dispatch({ type: "UPDATE_FORM", payload: data })
    },
  })
  return mutateForm
}
