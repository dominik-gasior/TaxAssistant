import { Button } from "@/components/ui/button"
import { Save } from "lucide-react"

interface SaveStepProps {
  onSave: () => void;
  disabled: boolean;
}

export default function SaveStep({ onSave, disabled }: SaveStepProps) {
  return (
    <Button onClick={onSave} disabled={disabled}>
      <Save className="mr-2 h-4 w-4" /> Zapisz
    </Button>
  )
}