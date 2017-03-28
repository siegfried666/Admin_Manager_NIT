#Frédéric CAZE-SULFOURT
#Mars 2017
#Neurones IT
#Programme Powershell pour lister les "owners" contenu dans un groupe de distribution spécifique
#--------------------------------------------------------------------------
# MODULE ACTIVE DIRECTORY
#--------------------------------------------------------------------------
 Import-Module ActiveDirectory -ErrorAction Stop
#--------------------------------------------------------------------------
#VARIABLES
#--------------------------------------------------------------------------
$KeyWordsForSearch = $args[0]
$Sortie = "C:\Users\FCazesulfourt\Documents\NIT_2017\Admin_Manager_NIT\powershell\tmp\" + "outputowner.txt"
#--------------------------------------------------------------------------
#FONCTION DE LANCEMENT DU PROGRAMME
#--------------------------------------------------------------------------
Function Start-Commands{List_Owners}
#--------------------------------------------------------------------------
#FONCTION DE LISTAGE DES OWNERS D'UNE LISTE DE DIFFUSION DE L'ACTIVE DIRECTORY
#--------------------------------------------------------------------------
Function List_Owners
{			
	Clear-Content $Sortie

	$KeyWordsForSearch = "*$KeyWordsForSearch*"

	Get-ADGroup -Filter {(GroupCategory -eq "Distribution") -and (Name -like $KeyWordsForSearch)} `
	-SearchBase "OU=GroupesDistributions,OU=Messagerie,DC=Neuronesit,DC=priv" `
	-Properties name,managedby `	 
	| select name,managedby `
	| Sort -Property Name `
	| format-table -autosize -hidetableheaders `
	| Out-File $Sortie
}

get-adgroup -Filter {(GroupCategory -eq "Distribution")} -SearchBase "OU=GroupesDistributions,OU=Messagerie,DC=Neuronesit,DC=priv" -property ManagedBy | %{(($_.ManagedBy -replace "\\,","~").split(",")[0] -replace "~",",").SubString(3)} | select $_.ManagedBy
#--------------------------------------------------------------------------
#PROGRAMME PRINCIPAL
#--------------------------------------------------------------------------
Start-Commands