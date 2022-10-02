using PicturifyRemaster.Core.Models.Images;
using PicturifyRemaster.Core.Processing;

namespace PicturifyRemaster.Core.Pipeline;

public record PipelineContext(IFastImage FastImage, IProcessorBase ProcessorBase);
