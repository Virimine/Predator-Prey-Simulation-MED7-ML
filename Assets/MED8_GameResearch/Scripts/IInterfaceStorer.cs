using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFunctionalityStorer {
	List<FunctionalityDrag> StorredFunctionalities { get; }


	void StoreFunctionality(FunctionalityDrag drag);
	bool IsFunctionalityAlreadyStored(FunctionalityDrag drag);
	void ClearStoredFunctionalities() { }

}
