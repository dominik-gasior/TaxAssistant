"use client"

import {
  TCompanyTaxpayer,
  TIndividualTaxpayer,
  TStepMid,
} from "@/app/types/steps"

import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { Label } from "../ui/label"
import { RadioGroup, RadioGroupItem } from "../ui/radio-group"
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/select"

export default function Step2({
  state,
  handleChange,
  handleNextStep,
  handlePreviousStep,
}: TStepMid) {
  const { formData } = state
  return (
    <form className="flex flex-col gap-2 ">

      <div className="flex flex-col gap-2 bg-muted p-4 border-b border-input">
        {/* FOR ALL address fields check if any of the address fields arent JUST "" and is so tell the user which one is still required */}
        {Object.entries(formData.address).map(([key, value]) => 
          { 
            // formate the keys to not have a _ and to start the first letter with capital but only the first letter of the key 
            const formattedKey = key.replace(/_/g, ' ').replace(/\b\w/g, char => char.toUpperCase());
            return value === "" ? <p key={key} className="text-sm text-red-500 mt-1">{`${formattedKey.split(' ').map(word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ')} jest obowiązkowe`}</p> : null}
        )}
      </div>
      <div className="flex flex-col gap-4 p-4 mb-4 border-b border-input">
        <h1 className="text-2xl font-bold">Dane podatnika</h1>
        <p className="text-sm text-gray-500 mb-4">
          Pola wymagane zostały oznaczone gwiazdką (
          <span className="text-red-500">*</span>)
        </p>

        <div className="mb-4">
          <Label className="block mb-2 font-semibold">
            Typ podatnika<span className="text-red-500">*</span>
          </Label>
          <RadioGroup
            value={formData.taxpayer_type}
            onValueChange={(value) => handleChange("taxpayer_type", value)}
            className="flex flex-col space-y-1"
          >
            <div className="flex items-center space-x-2">
              <RadioGroupItem value="individual" id="individual" />
              <Label htmlFor="individual">Osoba fizyczna</Label>
            </div>
            <div className="flex items-center space-x-2">
              <RadioGroupItem value="company" id="company" />
              <Label htmlFor="company">Firma</Label>
            </div>
          </RadioGroup>
        </div>

        {formData.taxpayer_type === "individual" && (
          <>
            <div className="mb-4">
              <Label htmlFor="first_name" className="block mb-2 font-semibold">
                Imię<span className="text-red-500">*</span>
              </Label>
              <Input
                type="text"
                id="first_name"
                name="first_name"
                required
                value={
                  (formData.taxpayer_data as TIndividualTaxpayer).first_name ||
                  ""
                }
                onChange={(e) =>
                  handleChange("taxpayer_data.first_name", e.target.value)
                }
              />
            </div>

            <div className="mb-4">
              <Label htmlFor="last_name" className="block mb-2 font-semibold">
                Nazwisko<span className="text-red-500">*</span>
              </Label>
              <Input
                type="text"
                id="last_name"
                name="last_name"
                required
                value={
                  (formData.taxpayer_data as TIndividualTaxpayer).last_name ||
                  ""
                }
                onChange={(e) =>
                  handleChange("taxpayer_data.last_name", e.target.value)
                }
              />
            </div>

            <div className="mb-4">
              <Label htmlFor="pesel" className="block mb-2 font-semibold">
                PESEL<span className="text-red-500">*</span>
              </Label>
              <Input
                type="text"
                id="pesel"
                name="pesel"
                required
                value={
                  (formData.taxpayer_data as TIndividualTaxpayer).pesel || ""
                }
                onChange={(e) =>
                  handleChange("taxpayer_data.pesel", e.target.value)
                }
              />
            </div>

            <div className="mb-4">
              <Label
                htmlFor="date_of_birth"
                className="block mb-2 font-semibold"
              >
                Data urodzenia<span className="text-red-500">*</span>
              </Label>
              <Input
                type="date"
                id="date_of_birth"
                name="date_of_birth"
                required
                value={
                  (formData.taxpayer_data as TIndividualTaxpayer)
                    .date_of_birth || ""
                }
                onChange={(e) =>
                  handleChange("taxpayer_data.date_of_birth", e.target.value)
                }
              />
            </div>
          </>
        )}

        {formData.taxpayer_type === "company" && (
          <>
            <div className="mb-4">
              <Label htmlFor="full_name" className="block mb-2 font-semibold">
                Pełna nazwa<span className="text-red-500">*</span>
              </Label>
              <Input
                type="text"
                id="full_name"
                name="full_name"
                required
                value={
                  (formData.taxpayer_data as TCompanyTaxpayer).full_name || ""
                }
                onChange={(e) =>
                  handleChange("taxpayer_data.full_name", e.target.value)
                }
              />
            </div>

            <div className="mb-4">
              <Label htmlFor="short_name" className="block mb-2 font-semibold">
                Skrócona nazwa
              </Label>
              <Input
                type="text"
                id="short_name"
                name="short_name"
                value={
                  (formData.taxpayer_data as TCompanyTaxpayer).short_name || ""
                }
                onChange={(e) =>
                  handleChange("taxpayer_data.short_name", e.target.value)
                }
              />
            </div>

            <div className="mb-4">
              <Label htmlFor="nip" className="block mb-2 font-semibold">
                NIP<span className="text-red-500">*</span>
              </Label>
              <Input
                type="text"
                id="nip"
                name="nip"
                required
                value={(formData.taxpayer_data as TCompanyTaxpayer).nip || ""}
                onChange={(e) =>
                  handleChange("taxpayer_data.nip", e.target.value)
                }
              />
            </div>
          </>
        )}
      </div>

      <div className="flex flex-col gap-4 p-4">
        <h2 className="text-xl font-bold mb-6">Adres</h2>

        <div className="mb-4">
          <Label htmlFor="country" className="block mb-2 font-semibold">
            Kraj<span className="text-red-500">*</span>
          </Label>
          <Select
            name="country"
            required
            value={formData.address.country}
            onValueChange={(value) => handleChange("address.country", value)}
          >
            <SelectTrigger className="w-full invalid:border-red-500 invalid:border-b">
              <SelectValue placeholder="Wybierz kraj" />
            </SelectTrigger>
            <SelectContent>
              {/*  */}
              <SelectItem value="light">Light</SelectItem>
              <SelectItem value="dark">Dark</SelectItem>
              <SelectItem value="system">System</SelectItem>
            </SelectContent>
          </Select>
    
        </div>

        <div className="mb-4">
          <Label htmlFor="province" className="block mb-2 font-semibold">
            Województwo<span className="text-red-500">*</span>
          </Label>
          <Input
            type="text"
            id="province"
            name="province"
            required
            value={formData.address.province}
            onChange={(e) => handleChange("address.province", e.target.value)}
          />
        
        </div>

        <div className="mb-4">
          <Label htmlFor="county" className="block mb-2 font-semibold">
            Powiat<span className="text-red-500">*</span>
          </Label>
          <Input
            type="text"
            id="county"
            name="county"
            required
            value={formData.address.county}
            onChange={(e) => handleChange("address.county", e.target.value)}
          />
          
        </div>

        <div className="mb-4">
          <Label htmlFor="municipality" className="block mb-2 font-semibold">
            Gmina<span className="text-red-500">*</span>
          </Label>
          <Input
            type="text"
            id="municipality"
            name="municipality"
            required
            value={formData.address.municipality}
            onChange={(e) =>
              handleChange("address.municipality", e.target.value)
            }
          />
         
        </div>

        <div className="mb-4">
          <Label htmlFor="street" className="block mb-2 font-semibold">
            Ulica
          </Label>
          <Input
            type="text"
            id="street"
            name="street"
            value={formData.address.street}
            onChange={(e) => handleChange("address.street", e.target.value)}
          />
       
        </div>

        <div className="mb-4">
          <Label htmlFor="house_number" className="block mb-2 font-semibold">
            Numer domu<span className="text-red-500">*</span>
          </Label>
          <Input
            type="text"
            id="house_number"
            name="house_number"
            required
            value={formData.address.house_number}
            onChange={(e) =>
              handleChange("address.house_number", e.target.value)
            }
          />
        
        </div>

        <div className="mb-4">
          <Label
            htmlFor="apartment_number"
            className="block mb-2 font-semibold"
          >
            Numer lokalu
          </Label>
          <Input
            type="text"
            id="apartment_number"
            name="apartment_number"
            value={formData.address.apartment_number}
            onChange={(e) =>
              handleChange("address.apartment_number", e.target.value)
            }
          />
       
        </div>

        <div className="mb-4">
          <Label htmlFor="city" className="block mb-2 font-semibold">
            Miejscowość<span className="text-red-500">*</span>
          </Label>
          <Input
            type="text"
            id="city"
            name="city"
            required
            value={formData.address.city}
            onChange={(e) => handleChange("address.city", e.target.value)}
          />
        
        </div>

        <div className="mb-4">
          <Label htmlFor="postal_code" className="block mb-2 font-semibold">
            Kod pocztowy<span className="text-red-500">*</span>
          </Label>
          <Input
            type="text"
            id="postal_code"
            name="postal_code"
            required
            value={formData.address.postal_code}
            onChange={(e) =>
              handleChange("address.postal_code", e.target.value)
            }
          />
         
        </div>
      </div>

      <div className="flex gap-4 mt-4 bg-muted p-4 border-t border-input">
        <Button
          type="button"
          variant="outline"
          className="font-bold"
          onClick={handlePreviousStep}
        >
          Poprzedni krok
        </Button>
        <Button type="button" className="font-bold" onClick={handleNextStep}>
          Następny krok
        </Button>
      </div>
    </form>
  )
}
