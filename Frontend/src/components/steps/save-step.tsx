import { Save } from "lucide-react"

import { Button } from "@/components/ui/button"

interface SaveStepProps {
  disabled: boolean
  className?: string
  handleSubmit: () => void
}

export default function SaveStep({
  disabled,
  className,
  handleSubmit,
}: SaveStepProps) {
  return (
    <Button type="button" className={className} onClick={handleSubmit} disabled={disabled}>
      <Save className="mr-2 h-4 w-4" /> Zapisz
    </Button>
  )
}
