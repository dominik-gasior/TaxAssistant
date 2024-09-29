import { Save } from "lucide-react"

import { useFormMutation } from "@/lib/hooks/use-form-mutation"
import { Button } from "@/components/ui/button"

interface SaveStepProps {
  disabled: boolean
  className?: string
}

export default function SaveStep({ disabled, className }: SaveStepProps) {
  const { mutate, isPending } = useFormMutation()
  const handleSubmit = () => {
    mutate()
  }
  // /update-form endpoint
  // use a mutation to update the form data on ther server and the use the response to update the state

  return (
    <Button className={className} onClick={handleSubmit} disabled={isPending || disabled}>
      <Save className="mr-2 h-4 w-4" /> Zapisz
    </Button>
  )
}
