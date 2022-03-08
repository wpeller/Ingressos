using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LogIDE.Dtos;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.LogIDE
{
	public interface ILogApiIDEService
	{
		Task RegistraLogAcesso(InputRegistraLogAcesso input);
		Task RegistraLogLogin(InputRegistraLogLoginDto input);
	}
}
